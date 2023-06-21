#deployment script
today=$(date +"%Y.%m.%d-%H.%M")
sudo cp -r /datadrive/IIFL/RecurrenceRewardWorker /datadrive/BackupIIFLCICD/RecurrenceRewardWorker${today}.bkp
sudo rm -rf /datadrive/IIFL/RecurrenceRewardWorker/*
sudo cp -r /home/azureadmin/azagent/_work/r364/a/_IIFL-RecurrenceRewardWorker-QA-CI/drop/RecurrenceRewardWorker.zip /datadrive/IIFL/RecurrenceRewardWorker/
cd /datadrive/IIFL/RecurrenceRewardWorker/
sudo unzip RecurrenceRewardWorker.zip
sudo chmod -R 755 *
sudo rm -r RecurrenceRewardWorker.zip
sudo systemctl restart IIFLRecurrenceRewardWorker.service