import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from '../core/guards/auth.guard';
import { PermissionsGuard } from '../core/guards/permissions.guard';
import { withPermission } from '../core/guards/with-permission';
import { MusicResolver } from '../core/resolvers/music.resolver';
import { PermissionType } from '../shared/models/permissionType';
import { LikedMusicsListComponent } from './components/liked-musics-list/liked-musics-list.component';
import { MusicCreateComponent } from './components/music-create/music-create.component';
import { MusicPlayComponent } from './components/music-play/music-play.component';
import { MusicsOwnListComponent } from './components/musics-own-list/musics-own-list.component';
import { OwnMusicsAnalysisComponent } from './components/own-musics-analysis/own-musics-analysis.component';
import { PlayedMusicsListsComponent } from './components/played-musics-lists/played-musics-lists.component';

const routes: Routes = [
  {
    path: 'play/:id',
    component: MusicPlayComponent,
    resolve:
    {
      music: MusicResolver
    },
  },
  { path: 'liked', component: LikedMusicsListComponent, canActivate: [AuthorizeGuard] },
  { path: 'history', component: PlayedMusicsListsComponent, canActivate: [AuthorizeGuard] },
  {
    path: 'own', children: [
      {
        path: '',
        component: MusicsOwnListComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.MusicCreator)
      },
      {
        path: 'analysis',
        component: OwnMusicsAnalysisComponent,
        canActivate: [PermissionsGuard],
        data: withPermission(PermissionType.MusicCreator)
      }
    ]

  },
  {
    path: 'create',
    component: MusicCreateComponent,
    canActivate: [PermissionsGuard],
    data: withPermission(PermissionType.MusicCreator)
  },
  {
    path: 'edit/:id',
    component: MusicCreateComponent,
    resolve:
    {
      music: MusicResolver
    },
    canActivate: [AuthorizeGuard],
    data: withPermission(PermissionType.MusicCreator)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MusicsRoutingModule { }
