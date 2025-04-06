import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  imports: [CommonModule,RouterModule,RouterOutlet], 
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
    this.router.navigate(['/login']);
    // window.location.reload(); // רענון פשוט

  }
}
