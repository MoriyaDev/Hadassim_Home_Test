import { Component, OnInit } from '@angular/core';
import { Supplier } from '../../../model/supplier.model';
import { Product } from '../../../model/product.model';
import { HttpClient } from '@angular/common/http';
import { OrderService } from '../../../services/order.service';
import { ProductService } from '../../../services/product.service';
import { SupplierService } from '../../../services/supplier.service';
import { OrderNew } from '../../../model/Order.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-order',
  imports: [ 
    FormsModule ],
  templateUrl: './add-order.component.html',
  styleUrl: './add-order.component.css'
})
export class AddOrderComponent implements OnInit  {
  suppliers: Supplier[] = [];
  selectedSupplier: Supplier | undefined;

  products: Product[] = [];
  selectedItems: { productName: string; quantity: number }[] = [];
  
  constructor(private orderService: OrderService,
    private productService :ProductService,
    private supplierService:SupplierService
  ) {
    this.loadSuppliers();
  }
  ngOnInit(): void {
    this.loadSuppliers();
  }
  getQuantity(productName: string): number {
    const item = this.selectedItems.find(i => i.productName === productName);
    return item ? item.quantity : 0;
  }
  

  loadSuppliers(): void {
    this.supplierService.loadSuppliers().subscribe({
      next: (data) => {
        this.suppliers = data;
        console.log(  this.suppliers);
        
      },
      error: (err) => {
        console.error('שגיאה בטעינת ספקים:', err);
      }
    });
  }
  onSupplierSelected(): void {
    if (!this.selectedSupplier) return;

    this.productService.getProductsBySupplier(this.selectedSupplier.id).subscribe({
      next: (data) => {
        this.products = data;
        this.selectedItems = []; // איפוס
      },
      error: (err) => {
        console.error('שגיאה בטעינת מוצרים:', err);
      }
    });
  }

  updateQuantity(product: Product, quantity: number): void {
    const existing = this.selectedItems.find(item => item.productName === product.name);
    if (existing) {
      existing.quantity = quantity;
    } else {
      this.selectedItems.push({
        productName: product.name,
        quantity: quantity
      });
    }
  }

  createOrder(): void {
    const order:OrderNew = {
      supplierName: this.selectedSupplier?.companyName!,
      createdAt: new Date().toISOString(),
      items: this.selectedItems.filter(item => item.quantity > 0)
    };

    this.orderService.createOrder(order).subscribe({
      next: () => {
        alert('🎉 הזמנה נשלחה בהצלחה!');
        this.products = [];
        this.selectedItems = [];
        this.selectedSupplier = undefined;
      },
      error: (err) => {
        console.error('שגיאה בשליחת הזמנה:', err);
        alert('⚠️ שגיאה בשליחת ההזמנה');
      }
    });
  }
}
