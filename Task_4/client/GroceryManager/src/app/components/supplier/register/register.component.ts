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
})
export class RegisterComponent implements OnInit{
  registerForm!: FormGroup;
  companyNameExists = false;


constructor( private fb: FormBuilder, private router: Router,private _supplierService :SupplierService) { 

  }
  ngOnInit(): void {
    this.registerForm = this.fb.group({
      companyName: ['', [Validators.required, Validators.minLength(2)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^05\d{8}$/)]],
      agentName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4)]],
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
        priceUnit: [0, [Validators.required, Validators.min(0.01)]],
        minQuantity: [1, [Validators.required, Validators.min(1)]]
      });
    }
    

  addProduct(): void {
    this.products.push(this.createProductGroup());
  }

  register(): void {
    this.companyNameExists = false;

    if (this.registerForm.invalid) return;

    this._supplierService.register(this.registerForm.value).subscribe({
      next: res => {
        console.log('ספק נרשם בהצלחה', res);
        this.router.navigate(['/login']);

      },
      error: err => {
        if(err === 500)
            this.companyNameExists = true;
          
      }
    });
  }

}
