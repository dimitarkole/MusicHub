import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { ModalComponent } from 'ngb-modal';

@Component({
  selector: 'app-send-confirm-email',
  templateUrl: './send-confirm-email.component.html',
  styleUrls: ['./send-confirm-email.component.css']
})
export class SendConfirmEmailComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ModalComponent>) { }

  ngOnInit() {
  }

  // If the user clicks the cancel button a.k.a. the go back button, then\
  // just close the modal
  closeModal() {
    this.dialogRef.close();
  }
}
