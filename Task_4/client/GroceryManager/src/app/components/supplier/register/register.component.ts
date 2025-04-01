import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SupplierService } from '../../../services/supplier.service';

@Component({
  standalone: true,
  selector: 'app-register',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  registerForm!: FormGroup;

constructor( private fb: FormBuilder, private router: Router,private _supplierService :SupplierService) { 

  }
  ngOnInit(): void {
    this.registerForm = this.fb.group({
      companyName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      agentName: ['', Validators.required],
      password: ['', Validators.required],
      products: this.fb.array([
        this.createProductGroup()
      ])
    });
  }

  get products(): FormArray {
    return this.registerForm.get('products') as FormArray;
  }

  createProductGroup(): FormGroup {
    return this.fb.group({
      name: ['', Validators.required],
      priceUnit: [0, Validators.required],
      minQuantity: [1, Validators.required]
    });
  }

  addProduct(): void {
    this.products.push(this.createProductGroup());
  }

  register(): void {
    if (this.registerForm.invalid) return;

    this._supplierService.register(this.registerForm.value).subscribe({
      next: res => {
        console.log('ספק נרשם בהצלחה', res);
      },
      error: err => {
        console.error('שגיאה ברישום', err);
      }
    });
  }

}
