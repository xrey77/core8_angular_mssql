import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
// import { MatIconModule } from '@angular/material/icon';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'; // Import FontAwesomeModule
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
// import { CounterComponent } from './counter/counter.component';
// import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DecimalPipe } from '@angular/common';

import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactusComponent } from './contactus/contactus.component';
import { SigninComponent } from './signin/signin.component';
import { SignupComponent } from './signup/signup.component';
import { ProfileComponent } from './profile/profile.component';
import { MfaComponent } from './mfa/mfa.component';
import { MessengerComponent } from './messenger/messenger.component';
import { CatalogComponent } from './products/catalog/catalog.component';
import { ListComponent } from './products/list/list.component';
import { SearchComponent } from './products/search/search.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AboutusComponent,
    ContactusComponent,
    SigninComponent,
    SignupComponent,
    ProfileComponent,
    MfaComponent,
    MessengerComponent,
    CatalogComponent,
    ListComponent,
    SearchComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FontAwesomeModule,
    FormsModule,
    DecimalPipe,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'aboutus', component: AboutusComponent },
      { path: 'contactus', component: ContactusComponent },
      { path: 'profile', component: ProfileComponent },   
      { path: 'productcatalog', component: CatalogComponent },   
      { path: 'listproducts', component: ListComponent },   
      { path: 'searchproducts', component: SearchComponent },   

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
