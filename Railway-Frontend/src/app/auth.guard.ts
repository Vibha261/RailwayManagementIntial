import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from './services/userService/user.service';

@Injectable({
 providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private userService: UserService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    console.log('AuthGuard called');
    const currentUser = this.userService.getCurrentUser();
    console.log('Current user:', currentUser);
    if (currentUser) {
       console.log('User is authenticated');
       return true;
    } else {
       console.log('User is not authenticated, redirecting to login');
       this.router.navigate(['/login']);
       return false;
    }
   }
   
}

