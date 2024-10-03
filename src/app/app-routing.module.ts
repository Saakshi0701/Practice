import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddBlogpostComponent } from './features/blogPosts/add-blogpost/add-blogpost.component';
import { AddCategoryComponent } from './features/categories/add-category/add-category.component';
import { BlogpostListComponent } from './features/blogPosts/blogpost-list/blogpost-list.component';
import { CategoryListComponent } from './features/categories/category-list/category-list.component';
import { EditCategoryComponent } from './features/categories/edit-category/edit-category.component';
import { EditBlogPostComponent } from './features/blogPosts/edit-blog-post/edit-blog-post.component';

const routes: Routes = [
  {
    path: 'admin/categories',
    component: CategoryListComponent
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent
  },
  {
    path: 'admin/categories/:id',
    component: EditCategoryComponent
  },
  {
    path: 'admin/blogposts',
    component: BlogpostListComponent
  },
  {
    path: 'admin/blogposts/add',
    component: AddBlogpostComponent
  },
  {
    path: 'admin/blogposts/:id',
    component: EditBlogPostComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
