import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogPost } from 'src/app/features/blogPosts/models/blog-post-model';
import { BlogPostsService } from '../services/blog-posts.service';


@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {

  blogPosts$? : Observable<BlogPost[]>;
  
  //to inject the service we are using constructor
  constructor(private blogPostService : BlogPostsService){

  }

  ngOnInit(): void {
    //get all blog posts from API
    this.blogPosts$ = this.blogPostService.getAllBlogPosts();
  }

}
