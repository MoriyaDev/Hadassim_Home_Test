import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { Order } from '../../../model/Order.model';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-orders',
  imports: [CommonModule]
  ,
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
  orders: Order[] = [];
  supplierId = Number(localStorage.getItem('supplierId'));

  constructor(private _orderService: OrderService) {}

  ngOnInit(): void {
    this._orderService.getOrdersBySupplier(this.supplierId).subscribe({
      next: (data) => {this.orders = data,console.log(data);
      },
      error: (err) => console.error('שגיאה בטעינת הזמנות', err)
    });
  }

  approve(orderId: number): void {
    this._orderService.approveOrder(orderId).subscribe(() => {
      const order = this.orders.find(o => o.id === orderId);
      if (order) order.status = 1; // InProcess
    });
  }

  getStatusText(status: number): string {
    switch (status) {
      case 0: return 'ממתינה';
      case 1: return 'בתהליך';
      case 2: return 'הושלמה';
      default: return 'לא ידוע';
    }
  }
}
