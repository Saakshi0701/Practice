import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/features/categories/models/category.model';
import { UpdateCategoryRequest } from 'src/app/features/categories/models/update-category-request-model';
import { CategoryService } from 'src/app/features/categories/services/category.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit{
  
  id: string | null = null;
  paramsSubscription?: Subscription;
  editCategorySubscription?: Subscription;
  category?: Category; 

  constructor(private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router) {
  }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if(this.id){
          //get the data from the API for this category Id
          this.editCategorySubscription = this.categoryService.getCategoryById(this.id)
          .subscribe ({
            next: (response) => {
                this.category = response;
            }
          })
        }
      }
    });
  }

  onFormSubmit(): void{
    const updateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    //pass this above obj to service 
    if(this.id)                //only call when the id is define
    {
    this.categoryService.updateCategory(this.id, updateCategoryRequest)
    .subscribe ({
      next: (response) => {
        this.router.navigateByUrl('admin/categories');
      }
    });
  }
}

onDelete(): void {
  if(this.id){
 this.categoryService.deleteCategory(this.id)
 .subscribe({
  next: (response) => {
    this.router.navigateByUrl('/admin/categories');
  }
})
}
}

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }
}
