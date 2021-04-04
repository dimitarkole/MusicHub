import { Component, Inject, inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from 'ngb-modal';
import Category from '../../models/category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category-delete',
  templateUrl: './category-delete.component.html',
  styleUrls: ['./category-delete.component.css']
})

export class CategoryDeleteComponent {
  constructor(public dialogRef: MatDialogRef<ModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private categoryService: CategoryService) {
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }

  deleteCategory() {
    this.categoryService.delete(this.data).subscribe(_ => {
      this.dialogRef.close();
    });
  }
}
