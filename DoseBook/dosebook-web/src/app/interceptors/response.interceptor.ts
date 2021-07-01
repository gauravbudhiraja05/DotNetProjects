import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavController } from '@ionic/angular';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ResponseInterceptor implements HttpInterceptor {

    constructor(private navContrl: NavController) {}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(map((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse && event.body && event.body.data) {
                    event = event.clone<any>({ body: event.body.data});
            }
            return event;
        }));
    }
}
