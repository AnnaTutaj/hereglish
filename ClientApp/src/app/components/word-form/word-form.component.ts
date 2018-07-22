import { CategoryService } from './../../services/category.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-word-form',
  templateUrl: './word-form.component.html',
  styleUrls: ['./word-form.component.css']
})
export class WordFormComponent implements OnInit {
  categories : any[];
  subcategories : any[];
  word: any = {};

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getCategories().subscribe(categories => 
      this.categories = categories
    );
  }

  onCategoryChange(){
    let selectedCategory = this.categories.find(c => c.id == this.word.category);
    this.subcategories = selectedCategory ? selectedCategory.subcategories : [];
  }

}
