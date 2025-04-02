import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  imports: [CommonModule, RouterOutlet], // ✅ הוספנו CommonModule
  standalone: true, 
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent  {
  title = 'GroceryManager';
  constructor( private router: Router) { 

  }
  isLoggedIn(): boolean {
    return !!localStorage.getItem('supplierId');
  }

  logout(): void {
    localStorage.removeItem('supplierId');
    this.router.navigate(['/order']);
    window.location.reload(); // רענון פשוט

  }
}
