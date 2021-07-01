import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
    providedIn: 'root'
})
export class RequestInterceptor implements HttpInterceptor {

    constructor(private authService: AuthenticationService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const headerConfig = {
            Accept: 'application/json',
            authorization: `Bearer ${this.authService.token}`
        };

        const request = req.clone({ setHeaders: headerConfig });
        return next.handle(request).pipe(
            tap(
                (event: HttpEvent<any>) => {},
                (err: any) => {
                    if (err instanceof HttpErrorResponse) {
                        console.log('error', err);
                    }
                }
            )
        );
    }
}
