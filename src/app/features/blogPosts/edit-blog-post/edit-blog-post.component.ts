import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPost } from 'src/app/features/blogPosts/models/blog-post-model';
import { Category } from 'src/app/features/categories/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';
import { CategoryService } from '../../categories/services/category.service';
import { UpdateBlogPost } from '../models/update-blog-post-model';
import { BlogPostsService } from '../services/blog-posts.service';

@Component({
  selector: 'app-edit-blog-post',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.css']
})
export class EditBlogPostComponent implements OnInit, OnDestroy{
   id: string | null = null;
   model? :BlogPost;
   categories$? : Observable<Category[]>;
   selectedCategories?: string[];
   isImageSelectorVisible : boolean = false;

   routeSubscription? : Subscription;
   updateBLogPostSubscription? : Subscription;
   getBLogPostSubscription? : Subscription;
   deleteBLogPostSubscription? : Subscription;
   imageSelectSubscription? : Subscription;

  constructor(private route: ActivatedRoute,
    private blogPostService: BlogPostsService,
    private categoryService: CategoryService,
    private router: Router,
    private imageService: ImageService) {
  }
 
  
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
     this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        //Get blogpost from api
        if(this.id){
          this.getBLogPostSubscription = this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories = response.categories.map(x => x.id);
            }
          });
          ;
        }
        this.imageSelectSubscription = this.imageService.onSelectImage()
        .subscribe({
          next: (response) => {
            if(this.model){
              this.model.featuredImageUrl = response.url;
              this.isImageSelectorVisible = false;
            }
          }
        })
      }
     })
  }

  onFormSubmit(): void{
    //Convert this model to Request object
    if(this.model && this.id){
      var updateBlogPost : UpdateBlogPost = {
        author : this.model.author,
        content : this.model.content,
        shortDescription : this.model.shortDescription,
        featuredImageUrl : this.model.featuredImageUrl,
        isVisible : this.model.isVisible,
        publishedDate : this.model.publishedDate,
        title : this.model.title,
        urlHandle : this.model.urlHandle,
        categories : this.selectedCategories ?? []
      };

      
      this.updateBLogPostSubscription = this.blogPostService.updateBlogPost(this.id, updateBlogPost)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      })
    }

  }

  onDelete(): void {
    if(this.id){
      //call service and delete blogpost
      this.deleteBLogPostSubscription = this.blogPostService.deleteBlogPost(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts');
        }
      });
    }
  }

  openImageSelector() : void {
      this.isImageSelectorVisible = true;
  }

  closeImageSelector() : void {
    this.isImageSelectorVisible = false;
  }


  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBLogPostSubscription?.unsubscribe();
    this.getBLogPostSubscription?.unsubscribe();
    this.deleteBLogPostSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }

}


