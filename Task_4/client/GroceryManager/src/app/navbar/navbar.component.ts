import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule], 
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  role: string | null = null;

  constructor(private router: Router) {}

  ngOnInit(): void {
    if (typeof window !== 'undefined') {
      this.role = localStorage.getItem('role');
      this.isLoggedIn();
    }
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('suppID');
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
    location.reload();


  }

  navigate(path: string): void {
    this.router.navigate([path]);
  }
}
