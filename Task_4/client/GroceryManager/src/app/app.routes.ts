import { Routes } from '@angular/router';
import { LoginComponent } from './components/supplier/login/login.component';
import { RegisterComponent } from './components/supplier/register/register.component';
import { OrdersComponent } from './components/supplier/orders/orders.component';
import { AddOrderComponent } from './components/owner/add-order/add-order.component';
import { InventoryComponent } from './components/owner/inventory/inventory.component';
import { AllOrderComponent } from './components/owner/all-order/all-order.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'signup', component: RegisterComponent },
  
    {
      path: 'order',
      component: OrdersComponent,
      canActivate: [AuthGuard, RoleGuard],
      data: { expectedRole: 'Supp' }
    },
    //OWNER
  
    {
      path: 'ad',
      component: AddOrderComponent,
      canActivate: [AuthGuard, RoleGuard],
      data: { expectedRole: 'Owner' }
    },
    {
      path: 'int',
      component: InventoryComponent,
      canActivate: [AuthGuard, RoleGuard],
      data: { expectedRole: 'Owner' }
    },
    {
      path: 'all',
      component: AllOrderComponent,
      canActivate: [AuthGuard, RoleGuard],
      data: { expectedRole: 'Owner' }
    }
    
];
