## Installing ASP.NET vNext

The instructions from the main ASP.net site are broken as of 12/4. 
Install KVM from the dev branch:

Windows: 
`@powershell -NoProfile -ExecutionPolicy unrestricted -Command "iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/aspnet/Home/dev/kvminstall.ps1'))"`

Linux: 
`curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/kvminstall.sh | sh && source ~/.kre/kvm/kvm.sh`

## Running the samples 

1. Run `kpm restore`
2. Navigate to src\helloworldweb
3. For WebListener: type "k web" and browse to http://localhost:5001
4. For Nowin: type "k nowin" and browse to http://localhost:5002
5. For Kestrel: type "k kestrel" and browse to http://localhost:5003