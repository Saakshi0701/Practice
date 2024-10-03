import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from 'src/app/features/categories/models/category.model';
import { CategoryService } from 'src/app/features/categories/services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  
  categories$ ?: Observable<Category[]>;
  
  constructor(private categoryService: CategoryService){
  
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    console.log(this.categories$);
  }
}

















// this.categoryService.getAllCategories()
// .subscribe(
//   {
//     next:(response)=>
//     {
//       this.categories$ = this.categories$;
//       console.log(this.categories$);
//     }
//   }
// )
// }