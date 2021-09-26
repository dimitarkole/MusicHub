export const globalConstants = {
  jwtTokenKey: "token",
  licensePath: "licenses",
  music: {
    imageFolderPathToUpload: "../../../../assets/resources/musics/images/",
    defauldImage: "MusicDefaultImage.jpg",
    imageFolderPath: "musics/images/",
    audioFolderPathUpload: "/assets/resources/musics/audio/",
    audioFolderPath: "musics/аudio/",
    nameMinLength: 1,
    nameMaxLength: 100,
    descriptionMinLength: 10,
    descriptionMaxLength: 1000,
  },
  playlist: {
    imageFolderPathToUpload: "../../../assets/resources/playlists/",
    defauldImage: "playlistDefaultImage.jpg",
  },
  pagination: {
    defaultPage: 1,
    itemsPerPage: 12,
    updateProcent: 0.75,
  },
	languageCode: 'en'
}


export const dateTimeFormats = {
  dateTime: 'MM/dd/yyyy HH:mm',
  date: 'MM/dd/yyyy',
}
