import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-order-by-drop-down',
  templateUrl: './order-by-drop-down.component.html',
  styleUrls: ['./order-by-drop-down.component.css']
})
export class OrderByDropDownComponent implements OnInit {
  @Output() orderMethod = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  changeOrderMehtod(orderMethod) {
    console.log(orderMethod)
    this.orderMethod.emit(orderMethod);
  }
}
