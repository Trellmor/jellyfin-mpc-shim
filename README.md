# Jellyfin MPC Shim

Jellyfin MPC Shim is a Windows cast client for Jellyfin. It will play your media files using [Media Player Classic - Home Cinema](https://github.com/clsid2/mpc-hc).

Similar to what [jellyfin-mpv-shim](https://github.com/jellyfin/jellyfin-mpv-shim) does.

## Setup

1. Install MPC, for example be installing the [K-Lite Codec Pack](https://codecguide.com/download_kl.htm)
1. Open MPC and make sure View/Options/Player/Web Interface "Listen on port" is enabled.
1. Start Jellyfin-mpc-shim.Tray
1. Configure Jellyfin and MPC Setting. Use tha same port as in 2.
1. Save settings
1. Use Jellyfin Web or another Jellyfin client to cast to "Jellyfin MPC Shim Tray"

## SyncPlay

You can use the systray menu to join a SyncPlay group.