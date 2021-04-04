import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule, NgbDropdownModule, NgbModalModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MatSidenavModule } from '@angular/material/sidenav';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { BaseUrlInterceptor } from './core/interceptors/base-url.interceptor';

import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { NavBarComponent } from './shared/nav-bar/nav-bar.component';
import { FooterComponent } from './shared/footer/footer.component';
import { CategoryCreateComponent } from './categories/components/category-create/category-create.component';
import { CategoryDeleteComponent } from './categories/components/category-delete/category-delete.component';
import { CategoryEditComponent } from './categories/components/category-edit/category-edit.component';
import { CategoriesListComponent } from './categories/components/categories-list/categories-list.component';
import { MatDialogModule } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavBarComponent,
    FooterComponent,
    SignUpComponent,
    SignInComponent,
    CategoryCreateComponent,
    CategoriesListComponent,
    CategoryDeleteComponent,
    CategoryEditComponent,
  ],
  entryComponents: [
    CategoryCreateComponent,
    CategoryDeleteComponent,
    CategoryEditComponent,
  ],
  imports: [
    MatSidenavModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDialogModule
  ],
  providers: [
    //{ provide: AppInsightsServiceService },
    NgbActiveModal,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
