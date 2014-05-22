import time
import threading
import os
import pwd
import grp
from client import Client

class BtsyncHelper:

        global client
        client = Client(host='127.0.0.1', port='8888', username='admin', password='zxcv1234')

        def get_folders(self):
            return client.sync_folders

        def check_folder(self, folder_path):
            for f in self.get_folders():
                if f['name'] == folder_path:
                    return True
            return False

        def create_folder(self, path):
            secret = client.generate_secret()
            return self.add_folder(path, secret['secret'])

        def add_folder(self, path, secret):
            if not os.path.exists(path):
                os.makedirs(path)

            if self.check_folder(path) == True:
                return 'Folder: ' + str(path) + ' already synchronized'

            uid = pwd.getpwnam('root').pw_uid
            os.chown(path, uid, -1)

            print 'Trying to open directory: ' + path
            client.add_sync_folder(path, secret)

            file = open(path + '/readme', 'a')
            file.write('This file automatically created for testing synchronization by BitTorrent Sync')
            file.close()
            os.chown(path + '/readme', uid, -1)

            return str(path) +  " created! Secret: " + secret
