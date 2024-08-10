import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import {ImageModule} from 'primeng/image';
import { AddAdviseComponent } from './add-advise/add-advise.component';
import { ListAdviseComponent } from './list/list-advise/list-advise.component';
import { ListContainerComponent } from './list/list-container/list-container.component';
import { AdviseService } from './service/advise.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { LoaderService } from './service/loader.service';
import { HttpClientModule, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ListService } from './list/list.service';
import { RestService } from './core/services/rest.service';
import { GridColumnFilterDirective } from './core/directives/grid-column-filter.directive';
import { MaskDirective } from './core/directives/mask.directive';
import { LimitlengthDirective } from './core/directives/limitlength.directive';
import { SinMaskPipe } from './core/pipe/sinMask.pipe';
import { ToastModule } from 'primeng/toast';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoaderComponent } from './shared/loader/loader.component';

@NgModule({
  declarations: [
    AppComponent,
    AddAdviseComponent,
    ListAdviseComponent,
    ListContainerComponent,
    GridColumnFilterDirective,
    MaskDirective,
    LimitlengthDirective,
    SinMaskPipe,
    LoaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    InputTextModule,
    InputMaskModule,
    TableModule,
    ButtonModule,
    ImageModule,
    CardModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ConfirmDialogModule,
    BrowserAnimationsModule,
    ToastModule
  ],
  exports: [
    GridColumnFilterDirective
  ],
  providers: [
    ListService,
    AdviseService,
    LoaderService,
    MessageService ,
    ConfirmationService,
    RestService,
    provideHttpClient(withInterceptorsFromDi())],
  bootstrap: [AppComponent]
})
export class AppModule { }
