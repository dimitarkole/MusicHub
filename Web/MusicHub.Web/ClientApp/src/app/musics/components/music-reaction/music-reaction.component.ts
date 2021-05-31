import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Reaction } from '../../../shared/models/reaction';
import { ReactionInfo } from '../../../shared/models/reactionInfo';
import { Music } from '../../models/music';
import { MusicReactionService } from '../../services/music-reaction.service';

@Component({
  selector: 'app-music-reaction',
  templateUrl: './music-reaction.component.html',
  styleUrls: ['./music-reaction.component.css']
})
export class MusicReactionComponent implements OnInit, OnChanges {
  @Input() music: Music;
  reactionForm: FormGroup;
  reaction: ReactionInfo;
  isAuth: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private musicReactionService: MusicReactionService,
    public authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.setReaction();
    this.reactionForm = this.formBuilder.group({
      musicId: this.music.id,
      reaction: null,
    });
  }

  ngOnInit(): void {
    this.setReaction();
    console.log(this.music);
    this.reactionForm = this.formBuilder.group({
      musicId: this.music.id,
      reaction: null,
    });
  }

  likeMusic() {
    this.reactionForm.get('reaction').setValue(Reaction.Like);
    let reaction: Music = this.reactionForm.value;
    reaction.reaction = Reaction.Like;
    this.musicReactionService.createReaction(reaction)
      .subscribe(_ => {
        this.music.countLikes++;
        this.setReaction();
      });
  }

  dislikeMusic() {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Music = this.reactionForm.value;
    reaction.reaction = Reaction.Dislike;
    this.musicReactionService.createReaction(reaction)
      .subscribe(_ => {
        this.music.countDislikes++;
        this.setReaction();
      });
  }

  unReactionMusic(id: string, oldReacton: string) {
    this.musicReactionService.delete(id)
      .subscribe(_ => {
        this.reaction.reaction = Reaction.None;
        if (oldReacton == "like") {
          this.music.countLikes--;
        }
        else if (oldReacton == "dislike") {
          this.music.countDislikes--;
        }
        this.setReaction();
      });
  }

  unLikeMusic(id: string) {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Music = this.reactionForm.value;
    reaction.reaction = Reaction.Dislike;
    this.musicReactionService.edit(reaction, id)
      .subscribe(_ => {
        this.music.countDislikes++;
        this.music.countLikes--;
        this.setReaction();
      });
  }

  unDislikeMusic(id: string) {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Music = this.reactionForm.value;
    reaction.reaction = Reaction.Like;
    this.musicReactionService.edit(reaction, id)
      .subscribe(_ => {
        this.music.countLikes++;
        this.music.countDislikes--;
        this.setReaction();
      });
  }

  get musicIdGet(): string {
    return this.music.id;
  }

  setReaction() {
    this.musicReactionService.isMusicReacted(this.musicIdGet).subscribe(data => {
      this.reaction = data
    });
  }
}
