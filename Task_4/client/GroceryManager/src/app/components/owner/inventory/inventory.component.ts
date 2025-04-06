import { Component, OnInit } from '@angular/core';
import { Inventory } from '../../../model/Inventory.model';
import { InventoryService } from '../../../services/inventory.service';

@Component({
  selector: 'app-inventory',
  imports: [],
  standalone: true,
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css'
})
export class InventoryComponent implements OnInit {
  inventory: Inventory[] = [];
  constructor(private inventoryService: InventoryService) {}
  ngOnInit(): void {
    this.loadInventory();
  }

loadInventory(): void {
    this.inventoryService.getAll().subscribe({
      next: (data) => {
        this.inventory = data;
        console.log(this.inventory);
        
      },
      error: (err) => {
        console.error('שגיאה בטעינת מלאי:', err);
      }
    });
  }

}
