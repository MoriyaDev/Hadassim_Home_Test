import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data['expectedRole'];
    const actualRole = localStorage.getItem('role');

    if (actualRole === expectedRole) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
