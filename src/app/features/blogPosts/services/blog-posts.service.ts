import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { AddBlogPost } from 'src/app/features/blogPosts/models/add-blog-post-model';
import { AddCategoryRequest } from 'src/app/features/categories/models/add-category-request.model';
import { BlogPost } from 'src/app/features/blogPosts/models/blog-post-model';
import { Category } from 'src/app/features/categories/models/category.model';
import { UpdateBlogPost } from '../models/update-blog-post-model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostsService {

  constructor(private http: HttpClient) { }

  CreateBlogPost(data: AddBlogPost): Observable<BlogPost> {
    return this.http.post<BlogPost>(`${environment.apiBaseUrl}api/BlogPosts/CreateBlogPost`,data);
  }

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(`${environment.apiBaseUrl}api/BlogPosts`);
  }

  getBlogPostById(id: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}api/BlogPosts/${id}`);
  }

  updateBlogPost(id : string, updateBlogPost : UpdateBlogPost) : Observable<BlogPost> {
    return this.http.put<BlogPost>(`${environment.apiBaseUrl}api/BlogPosts/${id}`,updateBlogPost);
  }

  deleteBlogPost(id: string): Observable<BlogPost>{
    return this.http.delete<BlogPost>(`${environment.apiBaseUrl}api/BlogPosts/${id}`);
  }

}

