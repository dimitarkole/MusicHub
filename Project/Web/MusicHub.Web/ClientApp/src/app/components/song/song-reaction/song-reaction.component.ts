import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Song } from '../../shared/models/song';
import { ReactionInfo } from '../../shared/models/reactionInfo';
import { SongReactionService } from '../../../core/services/song-reaction.service';
import { AuthService } from '../../../core/services/auth.service';
import { Reaction } from '../../shared/models/reaction';

@Component({
  selector: 'app-song-reaction',
  templateUrl: './song-reaction.component.html',
  styleUrls: ['./song-reaction.component.css']
})
export class SongReactionComponent implements OnInit, OnChanges {
  @Input() song: Song
  reactionForm: FormGroup;
  reaction: ReactionInfo;
  isAuth: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private songReactionService: SongReactionService,
    public authService: AuthService) {
    this.isAuth = authService.isAuth;
    this.authService.isAuthChanged.subscribe(() => {
      this.isAuth = this.authService.isAuth;
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.setReaction();
    this.reactionForm = this.formBuilder.group({
      songId: this.song.id,
      reaction: null,
    });
  }

  ngOnInit(): void {
    this.setReaction();
    console.log(this.song);
    this.reactionForm = this.formBuilder.group({
      songId: this.song.id,
      reaction: null,
    });
  }

  likeSong() {
    this.reactionForm.get('reaction').setValue(Reaction.Like);
    let reaction: Song = this.reactionForm.value;
    reaction.reaction = Reaction.Like;
    this.songReactionService.reactionSong(reaction)
      .subscribe(_ => {
        this.song.countLikes++;
        this.setReaction();
      });
  }

  dislikeSong() {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Song = this.reactionForm.value;
    reaction.reaction = Reaction.Dislike;
    this.songReactionService.reactionSong(reaction)
      .subscribe(_ => {
        this.song.countDislikes++;
        this.setReaction();
      });
  }

  unReactionSong(id: string, oldReacton: string) {
    this.songReactionService.delete(id)
      .subscribe(_ => {
        this.reaction.reaction = Reaction.None;
        if (oldReacton == "like") {
          this.song.countLikes--;
        }
        else if (oldReacton == "dislike") {
          this.song.countDislikes--;
        }
        this.setReaction();
      });
  }

  unLikeSong(id: string) {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Song = this.reactionForm.value;
    reaction.reaction = Reaction.Dislike;
    this.songReactionService.edit(reaction, id)
      .subscribe(_ => {
        this.song.countDislikes++;
        this.song.countLikes--;
        this.setReaction();
      });
  }

  unDislikeSong(id: string) {
    this.reactionForm.get('reaction').setValue(Reaction.Dislike);
    let reaction: Song = this.reactionForm.value;
    reaction.reaction = Reaction.Like;

    this.songReactionService.edit(reaction, id)
      .subscribe(_ => {
        this.song.countLikes++;
        this.song.countDislikes--;
        this.setReaction();
      });
  }

  get songIdGet(): string {
    return this.song.id;
  }

  setReaction() {
    this.songReactionService.isReactedSong(this.songIdGet).subscribe(data => {
      this.reaction = data
    });
  }
}
