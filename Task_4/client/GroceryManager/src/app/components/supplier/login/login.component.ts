import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SupplierService } from '../../../services/supplier.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true, 
  selector: 'app-login',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  public loginForm!: FormGroup;
  nameNotFound: boolean = false;
  wrongPassword: boolean = false;
  
  
  constructor( private router: Router,private _supplierService :SupplierService) { 

  }

  ngOnInit(): void {
   this.loginForm=new FormGroup({
    'companyName' :new FormControl('',Validators.required),
    'password':new FormControl('',Validators.required)
   })
  }

  login(){
    this.nameNotFound = false;
    this.wrongPassword = false;
  
    const { companyName, password } = this.loginForm.value;

    this._supplierService.login(companyName,password).subscribe({
      next: (supplier) => {
        console.log('התחברות הצליחה:', supplier);
          localStorage.setItem('supplierId', supplier.id.toString());
          localStorage.setItem('supplierName', supplier.companyName); 

        this.router.navigate(['/order']);
      },
      error: (err) => {
         // נניח שהשרת מחזיר הודעת שגיאה מסודרת
      if (err.error === 'UserNotFound') {
        this.nameNotFound = true;
      } else if (err.error === 'WrongPassword') {
        this.wrongPassword = true;
      }
      }
    });

  }

  signup() {
    this.router.navigate(['/signup']);
  }

}
