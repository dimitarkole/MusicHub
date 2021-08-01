import { Component, OnInit, EventEmitter, Output, OnChanges, SimpleChanges, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Permission } from '../models/permission';
import { PermissionType } from '../models/permissionType';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { BooleanSearchEntryMetadata } from '../models/boolean-search';
import { BooleanSearchService } from '../../core/resolvers/boolean-search.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit, OnChanges {
  @Output() sidenavToggle = new EventEmitter<void>();
  permitions: Permission[];
  isAuth: boolean = false;
  isAdmin: boolean = false;
  role: string = "";
  username: string = "";
  token: string = "";
  searchForm: FormGroup;

  userMappingFilterList = [
    ['LastName', "Last Name"],
    ['FirstName', "First Name"],
    ['MiddleName', "Middle Name"],
    ['Email', "Email"],
  ]

  musicMappingFilterList = [
    ['Name', 'Name'],
    ['Category', 'Category'],
  ]

  playlistMappingFilterList = [
    ['Name', 'Name'],
  ];

  isDarkModeActive = false;
  filter;
  currentFilterList;
  dropdownflag = true;
  searchQuery;
  notificationPopUpOpen = false;
  isLoggedIn: boolean;
  searchOptions: BooleanSearchEntryMetadata[] = [];
  originalSearchOptions: BooleanSearchEntryMetadata[] = [];
  //columns: BooleanSearchEntryMetadata[] = [];
  //originalColumns: BooleanSearchEntryMetadata[] = [];
  isSearchFocused = false;
  isSearchClosed = false;
  isEntityChoosen = false;
  profileImage = null;
  searchRedirectUrl: string = '';
  booleanSearchDocumentation: Map<string, BooleanSearchEntryMetadata[]> = new Map<string, BooleanSearchEntryMetadata[]>();

  constructor(
    private router: Router,
    private fb: FormBuilder,
    public authService: AuthService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private booleanSearchService: BooleanSearchService,
  ) {
   /* this.authService.getUserProfile().subscribe((data: any) => {
      this.profileImage = data.imageUrl;
    })
    /*this.authService.updatedUser.subscribe((data) => {
      this.profileImage = data.imageUrl;
    })*/
    this.getUserData();
    this.booleanSearchDocumentation = booleanSearchService.getBooleanSearchDocumentation();
    this.searchOptions = this.booleanSearchDocumentation.get('tables');
    this.originalSearchOptions = this.searchOptions;

    if (window.location.href.includes('login')) {
      this.isLoggedIn = false;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
     this.getUserData();
  }

  updateSearch() {
    if (this.type.value == "musics") {
      this.router.navigate([""], { queryParams: { searchQuery: this.search.value, type: this.type.value } });
    }
    this.router.navigate([this.type.value], { queryParams: { searchQuery: this.search.value, type: this.type.value } });
  }

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      search: [
        null,
      ],
      type: [
        "musics"
      ],
      orderMethod: [
        "CreatedOnDesc"
      ]
    });

    let url = window.location.href;
    this.route.queryParams.subscribe(queryParams => {
      if (queryParams) {
        if (queryParams.type) {
          this.type.setValue(queryParams.type);
        }
        this.search.setValue(queryParams.searchQuery);
      }
    });
    if (url.includes('users')) {
      this.currentFilterList = this.userMappingFilterList;
    } else if (url.includes('musics')) {
      this.currentFilterList = this.musicMappingFilterList;
    } else if (url.includes('playlists')) {
      this.currentFilterList = this.playlistMappingFilterList;
    }

    this.getUserData();
  }

  showNotifications() {
    this.notificationPopUpOpen = true;
  }

  hideNotificationWindow() {
    this.notificationPopUpOpen = false;
  }

  display(e: MouseEvent) {
    if (document.getElementById('dropdownCustom').style.display == 'none' || document.getElementById('dropdownCustom').style.display == '') {
      document.getElementById('dropdownCustom').style.display = 'block';
    } else {
      document.getElementById('dropdownCustom').style.display = 'none';
    }
  }

  logout() {
    this.authService.logout();
    this.authService.initializeAuthenticationState();
    this.router.navigate(['/']);
    location.reload();
  }

  // TODO: Refactor this.. it is fired 10 times per sec...
  get routerUrl() {
    return this.router.url;
  }

  toggleDarkMode() {
    if (!this.isDarkModeActive) {
      this.isDarkModeActive = !this.isDarkModeActive;
      (document.getElementsByTagName('body') as any)[0].classList.add('Dark-Mode');
    }
    else {
      this.isDarkModeActive = !this.isDarkModeActive;
      (document.getElementsByTagName('body') as any)[0].classList.remove('Dark-Mode');
    }
  }

  hasPermition(type: string): boolean {
    return this.permitions == null ? false : this.permitions.some(p => p.type == type);
  }

  get search(): AbstractControl {
    return this.searchForm.get('search');
  }

  get type(): AbstractControl {
    return this.searchForm.get('type');
  }

  get categoryId(): AbstractControl {
    return this.searchForm.get('categoryId');
  }

  get orderMethod(): AbstractControl {
    return this.searchForm.get('orderMethod');
  }

  private getUserData() {
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
      this.isAdmin = this.authService.isAdmin;
      this.role = this.authService.role;
      this.username = this.authService.username;
      this.authService.getPermissions().subscribe(p => {
        this.permitions = p;
      });
    });

    this.isAdmin = this.authService.isAdmin;
    this.isAuth = this.authService.isAuth;
    this.role = this.authService.role;
    this.username = this.authService.username;
  }
}
