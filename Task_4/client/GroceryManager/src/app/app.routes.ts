import { Routes } from '@angular/router';
import { LoginComponent } from './components/supplier/login/login.component';
import { RegisterComponent } from './components/supplier/register/register.component';
import { OrdersComponent } from './components/supplier/orders/orders.component';

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component:LoginComponent  },
    { path: 'signup', component:RegisterComponent }
    ,
    { path: 'order', component:OrdersComponent }
    ,
];
