import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AddCategoryRequest } from 'src/app/features/categories/models/add-category-request.model';
import { CategoryService } from 'src/app/features/categories/services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnDestroy {
  model: AddCategoryRequest;
  private addCategorySubscribtion? : Subscription;

  constructor(private categoryService: CategoryService,
    private router: Router) {
    this.model ={
      name: '',
      urlHandle: ''
    };
  }
  
  onFormSubmit(){
  this.addCategorySubscribtion = this.categoryService.addCategory(this.model)
    .subscribe
    ({
      next: (response) => 
      {
        this.router.navigateByUrl('/admin/categories');
      }
    })
  }
  
  ngOnDestroy(): void {
    this.addCategorySubscribtion?.unsubscribe();
  }
}
