import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
];

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
		FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    DatePipe
  ]
})
export class CategoriesModule { }
