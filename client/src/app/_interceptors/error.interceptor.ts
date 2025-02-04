import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

// The interceptor is executed whenever a http request is made, whatever if it's from the client to server or server to client
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router)
  const toastr = inject(ToastrService);

  // This returns an observable, so it has to be treated (in this case with pipe)
  return next(req).pipe(
    catchError(httpError => {
      if (httpError) {
        switch (httpError.status) {
          case 400:
            // 400 can be the validation error too (post), that returns an array in errors
            if (httpError.error.errors) {
              const modelStateErrors = [];
              for (const key in httpError.error.errors) {
                // This is for putting every error in a single array, to make easier to treat after
                if (httpError.error.errors[key])
                  modelStateErrors.push(httpError.error.errors[key])
              }
              // Flat() returns all the sub-arrays concatenated in one
              throw modelStateErrors.flat();
            }
            else {
              toastr.error(httpError.error);
            }
            break;
          case 401:
            toastr.error('Unauthorized', httpError.status);
            break;
          case 404:
            router.navigateByUrl('/not-found');
            break;
          case 500:
            // Navigation extras allow to pass states along the navigation, in this case with the details of the error (500 has details)
            const navigationExtras: NavigationExtras = {state: {error: httpError.error}}
            router.navigateByUrl('/server-error', navigationExtras);
            break;
          default:
            toastr.error('Something unexpected went wrong');
        }
      }
      throw httpError;
    })
  );
};
