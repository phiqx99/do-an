import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserProfileComponent } from './user-profile.component';
import { UProfileRouting } from './user-profile.routing';
import { UserService } from '../../../shared/service/userservice.service';
import { AuthGuard } from '../auth.guard';
import { JwtModule } from '@auth0/angular-jwt';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    UProfileRouting,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('userToken');
        },
        whitelistedDomains: ['localhost'],
        blacklistedRoutes: ['localhost/login']
      }
    })
  ],
  exports: [UserProfileComponent],
  declarations: [UserProfileComponent],
  providers: [UserService, AuthGuard,
  ]
})
export class UserProfileModule { }
