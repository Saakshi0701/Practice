import { Component, OnDestroy, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { AddBlogPost } from 'src/app/features/blogPosts/models/add-blog-post-model';
import { CategoryService } from '../../categories/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from 'src/app/features/categories/models/category.model';
import { BlogPostsService } from '../services/blog-posts.service';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit, OnDestroy{
  model: AddBlogPost;
  isImageSelectorVisible : boolean = false;
  categories$? : Observable<Category[]>;

  imageSelectorSubscription? : Subscription;

  constructor(private blogPostService: BlogPostsService,
    private router: Router,
    private categoryService : CategoryService,
    private imageService : ImageService){
    this.model = {
    title : '',
    shortDescription : '',
    content : '',
    featuredImageUrl : '',
    urlHandle : '',
    publishedDate : new Date(),
    author : '',
    isVisible :true,
    categories: []
    }
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.imageSelectorSubscription = this.imageService.onSelectImage()
    .subscribe({
      next: (selectedImage) => {
        this.model.featuredImageUrl = selectedImage.url;
        this.closeImageSelector();
      }
    })
  }


  onFormSubmit(): void {
    console.log(this.model);
    this.blogPostService.CreateBlogPost(this.model)
    .subscribe({
      next : (response) => {
        this.router.navigateByUrl('/admin/blogposts');
      }
    });
  }

  openImageSelector() : void {
    this.isImageSelectorVisible = true;
}

closeImageSelector() : void {
  this.isImageSelectorVisible = false;
}

ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
}
}
