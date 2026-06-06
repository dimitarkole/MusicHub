import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { ModalComponent } from 'ngb-modal';
import { globalConstants } from '../../../common/global-constants';
import { modelConstants } from '../../../common/model-constants';
import { MegaStorageFileService } from '../../../core/services/mega-storage-file.service';
import { CreatePrivacyStatus, PrivacyStatus, PrivacyStatusMapping } from '../../../shared/models/visibleStatus';
import { UserIpRule } from '../../models/userIpRule';
import { IpRuleType } from '../../models/ipRuleType';
import { IpRuleTypeMapping, CreateIpRuleType } from '../../models/ipRuleType';
import { UserIpRuleService } from '../../services/user-ip-rule.service';

@Component({
  selector: 'app-user-ip-rule-create',
  templateUrl: './user-ip-rule-create.component.html',
  styleUrls: ['./user-ip-rule-create.component.css']
})
export class UserIpRuleCreateComponent implements OnInit {
  ruleForm: FormGroup;

  ipRuleTypesMapping = IpRuleTypeMapping;
  ipRuleTypes = Object.keys(IpRuleType)
    .filter(k => !isNaN(Number(k)));;

  constructor(
    public dialogRef: MatDialogRef<UserIpRule>,
    private ruleService: UserIpRuleService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.ruleForm = this.formBuilder.group({
      pattern: [
        null,
        [Validators.required, Validators.maxLength(100)]
      ],
      ruleType: [
        IpRuleType.Allow,
        [Validators.required]
      ],
      description: [
        null,
        [Validators.maxLength(250)]
      ],
      isEnabled: [
        true
      ]
    });
  }

  onSubmit() {
    if (this.ruleForm.invalid) return;

    const rule = this.ruleForm.value;
    rule.ruleType = CreateIpRuleType(rule.ruleType);

    this.ruleService.Create(rule)
      .subscribe(_ => {
        this.dialogRef.close(true);
      });
  }
  closeModal() {
    this.dialogRef.close();
  }

  get name(): AbstractControl {
    return this.ruleForm.get('name');
  }

  get value(): AbstractControl {
    return this.ruleForm.get('value');
  }

  get pattern(): AbstractControl {
    return this.ruleForm.get('pattern');
  }

  get ruleType(): AbstractControl {
    return this.ruleForm.get('ruleType');
  }

  get description(): AbstractControl {
    return this.ruleForm.get('description');
  }

  get isEnabled(): AbstractControl {
    return this.ruleForm.get('isEnabled');
  }

  addRule() {
    // optional if used outside dialog
  }
}
