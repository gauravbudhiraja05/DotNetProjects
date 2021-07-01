import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';


@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    public authService: AuthenticationService,
    private router: Router
  ) {

  }

  async canActivate(): Promise<boolean> {
    const isLoggedIn = await this.authService.isLoggedIn();
    if (!isLoggedIn) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }

}
