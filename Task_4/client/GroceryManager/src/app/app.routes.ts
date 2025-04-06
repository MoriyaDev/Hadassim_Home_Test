import { Routes } from '@angular/router';
import { LoginComponent } from './components/supplier/login/login.component';
import { RegisterComponent } from './components/supplier/register/register.component';
import { OrdersComponent } from './components/supplier/orders/orders.component';
import { AddOrderComponent } from './components/owner/add-order/add-order.component';
import { InventoryComponent } from './components/owner/inventory/inventory.component';
import { AllOrderComponent } from './components/owner/all-order/all-order.component';

export const routes: Routes = [
    //supp
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component:LoginComponent  },
    { path: 'signup', component:RegisterComponent },
    { path: 'order', component:OrdersComponent },
    //owner
    { path: 'ad', component:AddOrderComponent },
    { path: 'int', component:InventoryComponent },
    { path: 'all', component:AllOrderComponent }
    
];
