import { Component } from '@angular/core';
import { Router, NavigationCancel, NavigationEnd } from '@angular/router';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { filter } from 'rxjs/operators';
import { NotificationHubService } from './core/services/hub-notification.service';
import { AuthenticationServices } from './core/services/authentication.service';
declare let $: any;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    providers: [
        Location, {
            provide: LocationStrategy,
            useClass: PathLocationStrategy
        }
    ],
})
export class AppComponent {
    location: any;
    routerSubscription: any;

    constructor(
        private router: Router,
        private readonly hubNotificationService: NotificationHubService,) {
    }

    ngOnInit() {
        this.recallJsFuntions();
        this.hubNotificationService.startConnection();
    }

    recallJsFuntions() {
        this.routerSubscription = this.router.events
            .pipe(filter(event => event instanceof NavigationEnd || event instanceof NavigationCancel))
            .subscribe(event => {
                this.location = this.router.url;
                if (!(event instanceof NavigationEnd)) {
                    return;
                }
                window.scrollTo(0, 0);
            });
    }
}