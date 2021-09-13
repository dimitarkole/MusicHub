export const globalConstants = {
  jwtTokenKey: "token",
  licensePath: "licenses",
  music: {
    imageFolderPath: "../../../../assets/resources/musics/images/",
    defauldImage: "MusicDefaultImage.jpg",
    imageFolderPathToUpload: "musics/images/",
    audioFolderPathUpload: "/assets/resources/musics/audio/",
    audioFolderPath: "musics/аudio/",
    nameMinLength: 1,
    nameMaxLength: 100,
    descriptionMinLength: 10,
    descriptionMaxLength: 1000,
  },
  playlist: {
    imageFolderPath: "../../../assets/resources/playlists/",
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
