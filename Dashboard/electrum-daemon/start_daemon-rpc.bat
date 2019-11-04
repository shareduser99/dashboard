@echo off
start C:\Users\Paul\source\repos\Dashboard\Dashboard\electrum-daemon\electrum-3.3.8-portable.exe daemon
timeout /T 5
start C:\Users\Paul\source\repos\Dashboard\Dashboard\electrum-daemon\electrum-3.3.8-portable.exe daemon load_wallet -w C:\Users\Paul\source\repos\Dashboard\Dashboard\electrum-daemon\electrum_data\wallets\electrum_wallet
timeout /T 10
exit