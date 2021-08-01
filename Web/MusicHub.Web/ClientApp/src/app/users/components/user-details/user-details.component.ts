import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../../core/services/auth.service';
import { RoleService } from '../../../roles/services/role.service';
import { Role } from '../../../shared/models/role';
import User from '../../../shared/models/user';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  isAuth: boolean = false;
  roles: Role[] = [];
  selectedRoles: Role[] = [];
  @Input() user: User;
  roleForm: FormGroup

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private roleService: RoleService,
    public authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnInit(): void {
    this.roleService.getAll().subscribe(r => {
      this.roles = r;
      this.route.params.subscribe(routeParams => {
        this.roleForm = this.formBuilder.group({
          roleIds: this.formBuilder.array([])
        });
        this.user = this.route.snapshot.data.user;
        var roles = this.user.roles;
        roles.forEach(r => {
          var role = this.roles.filter(p => p.name == r)[0];
          const roleForm = this.formBuilder.group({
            roleId: role.id,
            roleName: role.name,
          });
          this.roleIds.push(roleForm);
          this.selectedRoles.push(role);
          this.roles = this.roles.filter(p => p.id != role.id);
        });
      });
    });
  }

  OnSubmit() {
    let user: User = this.roleForm.value;
    let userId: string = this.user.id;
    this.authService.changeRoles(userId, user)
      .subscribe(_ => {
        window.location.reload();
      })
  }

  addRole() {
    //element id -> select-permissions
    var e = (document.getElementById("select-roles")) as HTMLSelectElement;
    var sel = e.selectedIndex;
    var opt = e.options[sel];
    var curValue = opt.value;
    var curValueNumber = curValue;
    var role = this.roles.filter(p => p.id == curValueNumber)[0];
    const roleForm = this.formBuilder.group({
      roleId: curValue,
      roleName: role.name,
    });

    this.roleIds.push(roleForm);
    this.selectedRoles.push(role);
    this.roles = this.roles.filter(p => p.id != curValueNumber);
  }

  removeRole(role: Role) {
    this.selectedRoles = this.selectedRoles.filter(p => p.id != role.id);
    this.roleIds.removeAt(this.roleIds.value.findIndex(p => p.roleId === role.id))
    this.roles.push(role);
    //this.roles = this.roles.sort((a, b) => a.name.localeCompare(b.name))
  }

  get roleIds() {
    return this.roleForm.get('roleIds') as FormArray;
  }
}
