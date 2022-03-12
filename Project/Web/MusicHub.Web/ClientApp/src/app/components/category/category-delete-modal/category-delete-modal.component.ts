import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-category-delete-modal',
  templateUrl: './category-delete-modal.component.html',
  styleUrls: ['./category-delete-modal.component.css']
})

export class CategoryDeleteModalComponent {
  constructor(public modal: NgbActiveModal,
    private router: Router,
    private authService: AuthService) {
    if ((authService.isAuth == false) || (authService.isAdmin == false)) {
      this.router.navigate(['']);
    }
  }
}
