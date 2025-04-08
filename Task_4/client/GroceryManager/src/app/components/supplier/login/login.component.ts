import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SupplierService } from '../../../services/supplier.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  public loginForm!: FormGroup;
  nameNotFound: boolean = false;
  wrongPassword: boolean = false;


  constructor(private router: Router, private _supplierService: SupplierService) {

  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      'companyName': new FormControl('', Validators.required),
      'password': new FormControl('', Validators.required)
    })
  }

  login() {
    this.nameNotFound = false;
    this.wrongPassword = false;

    const { companyName, password } = this.loginForm.value;

    this._supplierService.login(companyName, password).subscribe({
      next: (res) => {
        console.log('התחברות הצליחה:', res);
  
        localStorage.setItem('supplierId', res.suppID);
        localStorage.setItem('supplierName', res.name);
        localStorage.setItem('token', res.token);
        localStorage.setItem('role', res.role);
  
        const path = res.role === 'Owner' ? '/all' : '/order';
      this.router.navigate([path]).then(() => {
        location.reload(); 
      });

      },
      error: (error) => {
        console.log('שגיאה בהתחברות:', error);
          const errorMessage = error.error;
          if (errorMessage.message === 'UserNotFound') {
            this.nameNotFound = true;
          } else if (errorMessage.message === 'WrongPassword') {
            this.wrongPassword = true;
          }
        
      }
    });

  }

  signup() {
    this.router.navigate(['/signup']);
  }

}
