import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';

// Services in Angular are by default singletons: They initialize themselves at the start of the application and dispose at the end of the running app, which means services are accessible all over across the app;
// Injectable with providedIn: 'root' means that the service is available for dependency injection in the classes that are part of Angular's DI (dependency injection), such as components, directives, etc;
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/'
  // Signal for the verification: if there is any user logged
  currentUser = signal<User | null>(null);

  public login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user) {
          // Set the local storage with the variable that I want, so when refreshing the user does not have to login again
          localStorage.setItem('user', JSON.stringify(user));
          // Set the signal value with the user returned from the post
          this.currentUser.set(user);
        }
        // If returning just the map function, the object is gonna be undefined to be consoled.log(), so we can just the user        
        return user;
      })
    );
  }

  public register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user) {
          // Here is gonna be the same as the login, because we want the user to be and stay logged
          localStorage.setItem('user', JSON.stringify(user));
          // Set the signal value with the user returned from the post
          this.currentUser.set(user);
        }
        return user;
      })
    );
  }

  public logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
