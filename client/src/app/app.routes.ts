import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './_errors/test-errors/test-errors.component';
import { NotFoundComponent } from './_errors/not-found/not-found.component';
import { ServerErrorComponent } from './_errors/server-error/server-error.component';

// Routes that are used to navegate between different components with different routes, using RouterOutlet
export const routes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        // Here I can specify the routes that are gonna follow the authorization above
        children: [
            { path: 'members', component: MemberListComponent },
            { path: 'members/:id', component: MemberDetailComponent },
            { path: 'lists', component: ListsComponent },
            { path: 'messages', component: MessagesComponent },
        ]
    },
    { path: 'errors', component: TestErrorsComponent},
    { path: 'not-found', component: NotFoundComponent},
    { path: 'server-error', component: ServerErrorComponent},
    // When it doesn't find the route in the route list, it loads HomeComponent
    { path: '**', component: HomeComponent, pathMatch: 'full' },
];
