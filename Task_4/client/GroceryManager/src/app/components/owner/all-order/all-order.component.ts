import { Component } from '@angular/core';
import { Order } from '../../../model/Order.model';
import { OrderService } from '../../../services/order.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-all-order',
  imports: [FormsModule],
  templateUrl: './all-order.component.html',
  styleUrl: './all-order.component.css'
})
export class AllOrderComponent {
  orders: Order[] = [];
  selectedStatus: number | null = null; // null מציג הכול

    constructor(private _orderService: OrderService) {}

    ngOnInit(): void {
      this._orderService.getOrders().subscribe({
        next: (data) => 
          {this.orders = data,
            console.log(data);
          this.orders = data.sort((a, b) => {
            return new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime();
          });
          
        },
        error: (err) => console.error('שגיאה בטעינת הזמנות', err)
      });
    }

    complete(orderId: number): void {
      this._orderService.completeOrder(orderId).subscribe(() => {
        const order = this.orders.find(o => o.id === orderId);
        if (order) order.status = 2; 
      });
    }
    getStatusText(status: number): string {
      switch (status) {
        case 0: return '🕓 ממתינה';
      case 1: return '🔄 בתהליך';
      case 2: return '✅ הושלמה';
      default: return '❓ לא ידוע';
      }
    }

    get filteredOrders() {
      if (this.selectedStatus === null) {
        return this.orders;
      }
      return this.orders.filter(order => order.status === this.selectedStatus);
    }

}
