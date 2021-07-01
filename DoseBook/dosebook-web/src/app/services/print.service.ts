import { CommonModule } from '@angular/common';
import { Compiler, ComponentFactory, ComponentFactoryResolver, EmbeddedViewRef, Injectable, Injector, NgModule, ViewContainerRef } from '@angular/core';
import { Printer, PrintOptions } from '@ionic-native/printer/ngx';
import { Platform } from '@ionic/angular';
import { DashboardPageModule } from '../dashboard/dashboard.module';
import { PrintLayoutComponent } from '../dashboard/print-layout/print-layout.component';
import { Prescription } from '../models/prescription.model';
import { AgePipe } from '../pipes/age.pipe';
import { SharedModule } from '../shared.module';

@Injectable({
  providedIn: 'root'
})
export class PrintService {

  constructor(private _compiler: Compiler,
              private componentFactoryResolver: ComponentFactoryResolver,
              private printer: Printer,
              private platform: Platform,
              private injector: Injector) {

              this.platform.ready().then((p) => {
                console.log(p);
              });
  }

  async printDocument(prx: Prescription): Promise<boolean> {
    // // check if it can be replaced with entryComponents in AppModule
    // @NgModule({declarations: [PrintLayoutComponent], imports: [ CommonModule, SharedModule ]})
    // class TemplateModule {}

    // const mod = this._compiler.compileModuleAndAllComponentsSync(TemplateModule);
    // const factory: ComponentFactory<PrintLayoutComponent> = mod.componentFactories.find((comp) =>
    //   comp.componentType === PrintLayoutComponent
    // );

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(PrintLayoutComponent);
    const componentRef = componentFactory.create(this.injector);
    componentRef.instance.prx = prx;

    // // important to detect changes
    componentRef.changeDetectorRef.detectChanges();

    const domElement = componentRef.location.nativeElement.innerHTML;

    const options: PrintOptions = {
      name: 'Prescription',
      duplex: true,
      orientation: 'portrait',
      monochrome: true
    };

    this.printWeb(domElement);

    return true;

    // if (this.platform.is('desktop') || this.platform.is('mobileweb')) {
    //   this.printWeb(domElement);
    //   return true;
    // } else {
    //   return this.printer.print(domElement, options).then((success) => {
    //     return true;
    //   }, err => {
    //     console.error(err);
    //     return false;
    //   }).catch(error => {
    //     console.error(error);
    //     return false;
    //   });
    // }

  }

  private printWeb(domElement: string) {
    const printWin = window.open();
    printWin.document.write(domElement);
    printWin.document.close();
    printWin.focus();
    printWin.print();
    printWin.close();
  }
}
