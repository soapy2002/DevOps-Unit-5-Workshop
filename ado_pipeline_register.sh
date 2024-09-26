#!/bin/sh

mkdir /agent && cd /agent
sudo chown azureuser /agent

wget https://vstsagentpackage.azureedge.net/agent/3.243.0/vsts-agent-linux-x64-3.243.0.tar.gz

tar zxvf vsts-agent-linux-x64-3.243.0.tar.gz
echo "Reached here"
su - azureuser -c "cd /agent && /agent/config.sh --unattended --url $1 --auth pat --token $2 --pool default --agent myAgent --acceptTeeEula --work /agent"
echo "Command complete"
echo $1 $2
sudo /agent/svc.sh install
sudo /agent/svc.sh start