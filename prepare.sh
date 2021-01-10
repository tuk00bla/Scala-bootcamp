export DEBIAN_FRONTEND=noninteractive

apt update
apt install -y wget

wget https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb \
    -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb

apt update && apt-get install -y apt-transport-https
apt update && apt-get install -y dotnet-sdk-5.0
apt update && apt-get install -y dotnet-runtime-5.0
dotnet build