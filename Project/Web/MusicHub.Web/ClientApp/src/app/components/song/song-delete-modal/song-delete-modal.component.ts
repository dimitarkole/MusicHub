import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-song-delete-modal',
  templateUrl: './song-delete-modal.component.html',
  styleUrls: ['./song-delete-modal.component.css']
})
export class SongDeleteModalComponent {
  constructor(public modal: NgbActiveModal,
    private router: Router,
    private authService: AuthService) {
    if (authService.isAuth == false) {
      this.router.navigate(['']);
    }
  }
}
