import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import * as Sentry from '@sentry/angular';
import { Integrations } from '@sentry/tracing';

Sentry.init({
  dsn: 'https://1d93235f9dc34799aab084bda5d57093@o460066.ingest.sentry.io/5556876',
  autoSessionTracking: true,
  integrations: [
    new Integrations.BrowserTracing({
      tracingOrigins: ['https://dosebook.doctor.com'],
      routingInstrumentation: Sentry.routingInstrumentation,
    }),
  ],

  // We recommend adjusting this value in production, or using tracesSampler
  // for finer control
  tracesSampleRate: 1.0,
});



if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.log(err));
